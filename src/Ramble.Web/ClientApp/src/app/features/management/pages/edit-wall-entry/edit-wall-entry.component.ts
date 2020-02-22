import { Component, OnInit, OnDestroy } from '@angular/core';
import { Apollo } from 'apollo-angular';
import { Router, ActivatedRoute } from '@angular/router';
import { FormBuilder, Validators, AbstractControl } from '@angular/forms';
import { ManagementApiService } from '../../services/management-api.service';
import gql from 'graphql-tag';
import { Subscription } from 'rxjs';

@Component({
  selector: 'app-edit-wall-entry',
  templateUrl: './edit-wall-entry.component.html',
  styleUrls: ['./edit-wall-entry.component.scss']
})
export class EditWallEntryComponent implements OnInit, OnDestroy {
  isLoading = true;
  isCreatingEntry: boolean;

  entryForm = this.fb.group({
    content: ['', Validators.required]
  });

  wallId: number;
  wallEntryId: number;
  subscriptions = new Subscription();

  constructor(
    private apollo: Apollo,
    private router: Router,
    private route: ActivatedRoute,
    private fb: FormBuilder,
    private managementApi: ManagementApiService,
  ) { }

  ngOnInit() {
    this.wallId = Number(this.route.snapshot.params.wid);
    this.wallEntryId = this.route.snapshot.params.eid ? this.route.snapshot.params.eid : -1;

    if (this.wallEntryId > 0) {
      this.isCreatingEntry = false;
      this.subscriptions.add(this.apollo
        .watchQuery<any>({
          query: gql`
            query GetWallEntry($id: Int!) {
              wallEntry(id: $id) {
                id
                type: entryType
                content: entryContent
                timestamp: entryTimestamp
              }
            }
          `,
          variables: {
            id: this.wallId
          }
        })
        .valueChanges.subscribe(result => {
          this.isLoading = false;
          this.entryForm.setValue({
            content: result.data.wallEntry.content
          });
        }));
    } else {
      this.isCreatingEntry = true;
      this.isLoading = false;
    }
  }

  ngOnDestroy(): void {
    this.subscriptions.unsubscribe();
  }

  saveEntry() {
    const contentControl = this.entryForm.get('content') as AbstractControl;
    if (this.isCreatingEntry) {
      this.managementApi.createWallEntry(this.wallId, contentControl.value)
        .subscribe(res => {
          this.router.navigate(['/management/walls', this.wallId]);
        });
    } else {
      this.managementApi.updateWallEntry(this.wallId, this.wallEntryId, contentControl.value)
        .subscribe(res => {
          this.router.navigate(['/management/walls', this.wallId]);
        });
    }
  }
}
