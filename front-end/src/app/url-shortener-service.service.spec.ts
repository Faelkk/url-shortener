import { TestBed } from '@angular/core/testing';

import { UrlShortenerServiceService } from './url-shortener-service.service';

describe('UrlShortenerServiceService', () => {
  let service: UrlShortenerServiceService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(UrlShortenerServiceService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
