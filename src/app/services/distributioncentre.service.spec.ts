import { TestBed } from '@angular/core/testing';

import { DistributioncentreService } from './distributioncentre.service';

describe('DistributioncentreService', () => {
  let service: DistributioncentreService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(DistributioncentreService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
