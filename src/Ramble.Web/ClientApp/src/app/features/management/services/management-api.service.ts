import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { CreateWallModel, CreateWallEntryModel, WallEntryType, UpdateWallEntryModel } from './models/wall-models';
import { ManagementServicesModule } from '../management-services.module';

@Injectable({
  providedIn: ManagementServicesModule
})
export class ManagementApiService {

  constructor(
    private httpClient: HttpClient
  ) { }

  createWall(name: string): Observable<number> {
    const request: CreateWallModel = {
      name: name
    };

    return this.httpClient.post<number>('api/v1/management/walls', request);
  }

  createWallEntry(wallId: number, content: string): Observable<number> {
    const request: CreateWallEntryModel = {
      entryType: WallEntryType.Text,
      entryContent: content
    };

    return this.httpClient.post<number>(`api/v1/management/walls/${wallId}/entries`, request);
  }

  updateWallEntry(wallId: number, wallEntryId: number, content: string): Observable<any> {
    const request: UpdateWallEntryModel = {
      entryContent: content
    };

    return this.httpClient.post(`api/v1/management/walls/${wallId}/entries/${wallEntryId}`, request);
  }
}
