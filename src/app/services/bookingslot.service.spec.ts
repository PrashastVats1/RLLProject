import { TestBed } from '@angular/core/testing';

import { BookingslotService } from './bookingslot.service';

describe('BookingslotService', () => {
  let service: BookingslotService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(BookingslotService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
