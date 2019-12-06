import { Component, OnInit } from '@angular/core';
import { Apollo } from 'apollo-angular';
import { WallModel } from 'src/app/shared/models';
import gql from 'graphql-tag';

@Component({
  selector: 'app-wall-overview',
  templateUrl: './wall-overview.component.html',
  styleUrls: ['./wall-overview.component.scss']
})
export class WallOverviewComponent implements OnInit {

  walls: WallModel[] = [];

  constructor(
    private apollo: Apollo
  ) { }

  ngOnInit() {
    this.apollo
      .watchQuery<any>({
        query: gql`
          {
            walls {
              id
              name
            }
          }
        `,
      })
      .valueChanges.subscribe(result => {
        this.walls = result.data.walls as WallModel[];
      });
  }
}
