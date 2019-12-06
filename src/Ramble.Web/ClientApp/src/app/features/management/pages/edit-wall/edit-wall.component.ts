import { Component, OnInit, OnDestroy } from '@angular/core';
import { Apollo } from 'apollo-angular';
import { ActivatedRoute } from '@angular/router';
import gql from 'graphql-tag';
import { Subscription } from 'rxjs';

class WallModel {
  id: number;
  name: string;
  entries: WallEntityModel[];
}

class WallEntityModel {
  id: number;
  type: number;
  timestamp: Date;
}

@Component({
  selector: 'app-edit-wall',
  templateUrl: './edit-wall.component.html',
  styleUrls: ['./edit-wall.component.scss']
})
export class EditWallComponent implements OnInit, OnDestroy {
  wall: WallModel = new WallModel();
  subscriptions = new Subscription();

  constructor(
    private apollo: Apollo,
    private route: ActivatedRoute
  ) { }

  ngOnInit() {
    const wallId = Number(this.route.snapshot.params.wid);
    const query = this.apollo.watchQuery<any>({
      query: gql`
          query GetWall($id: Int!) {
            wall(id: $id) {
              id
              name
              entries: wallEntries {
                id
                type: entryType
                timestamp: entryTimestamp
              }
            }
          }
        `,
      variables: {
        id: wallId
      }
    });

    this.subscriptions.add(query.valueChanges.subscribe(result => {
      this.wall = result.data.wall as WallModel;
    }));

    if (query.currentResult) {
      query.refetch();
    }
  }

  ngOnDestroy(): void {
    this.subscriptions.unsubscribe();
  }
}
