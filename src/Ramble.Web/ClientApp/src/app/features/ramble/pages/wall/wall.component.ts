import { Component, OnInit } from '@angular/core';
import { Apollo } from 'apollo-angular';
import { ActivatedRoute } from '@angular/router';
import gql from 'graphql-tag';

class WallModel {
  id: number;
  name: string;
  entries: WallEntityModel[];
}

class WallEntityModel {
  id: number;
  type: number;
  content: string;
  timestamp: Date;
}

@Component({
  selector: 'app-wall',
  templateUrl: './wall.component.html',
  styleUrls: ['./wall.component.scss']
})
export class WallComponent implements OnInit {
  wall: WallModel;

  constructor(
    private apollo: Apollo,
    private route: ActivatedRoute
  ) { }

  ngOnInit() {
    const wallId = Number(this.route.snapshot.params.wid);
    this.apollo
      .watchQuery<any>({
        query: gql`
          query GetWall($id: Int!) {
            wall(id: $id) {
              id
              name
              entries: wallEntries {
                id
                type: entryType
                content: entryContent
                timestamp: entryTimestamp
              }
            }
          }
        `,
        variables: {
          id: wallId
        }
      })
      .valueChanges.subscribe(result => {
        this.wall = result.data.wall as WallModel;
      });
  }
}
