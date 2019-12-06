import { Component, OnInit } from '@angular/core';
import { FormControl, Validators } from '@angular/forms';
import { ManagementApiService } from '../../services/management-api.service';
import { Router } from '@angular/router';
import { Apollo } from 'apollo-angular';
import gql from 'graphql-tag';
import { WallModel } from 'src/app/shared/models';

@Component({
  selector: 'app-walls',
  templateUrl: './walls.component.html',
  styleUrls: ['./walls.component.scss']
})
export class WallsComponent implements OnInit {
  isCreateWallModalActive = false;
  newWallName = new FormControl('', Validators.required);

  walls: WallModel[] = [];

  constructor(
    private apollo: Apollo,
    private managementApi: ManagementApiService,
    private router: Router
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

  hideCreateWallModal() {
    this.isCreateWallModalActive = false;
  }

  showCreateWallModal() {
    this.isCreateWallModalActive = true;
  }

  createWall() {
    if (this.newWallName.invalid) {
      return;
    }

    this.managementApi.createWall(this.newWallName.value).subscribe(
      e => this.router.navigate(['/management', 'walls', e])
    );
  }
}
