import { TestBed } from '@angular/core/testing';

import { StockvaccineService } from './stockvaccine.service';

describe('StockvaccineService', () => {
  let service: StockvaccineService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(StockvaccineService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
