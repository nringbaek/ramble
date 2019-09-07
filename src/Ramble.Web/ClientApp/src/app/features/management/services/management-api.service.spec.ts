import { TestBed } from '@angular/core/testing';

import { ManagementApiService } from './management-api.service';

describe('ManagementApiService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: ManagementApiService = TestBed.get(ManagementApiService);
    expect(service).toBeTruthy();
  });
});
