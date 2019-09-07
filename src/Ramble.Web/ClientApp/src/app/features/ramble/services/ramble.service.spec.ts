import { TestBed } from '@angular/core/testing';

import { RambleService } from './ramble.service';

describe('RambleService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: RambleService = TestBed.get(RambleService);
    expect(service).toBeTruthy();
  });
});
