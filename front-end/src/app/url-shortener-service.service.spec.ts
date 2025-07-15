import { TestBed } from '@angular/core/testing';
import { UrlShortenerServiceService } from './url-shortener-service.service';
import { provideHttpClient, withFetch } from '@angular/common/http';

describe('UrlShortenerServiceService', () => {
  let service: UrlShortenerServiceService;

  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [
        UrlShortenerServiceService,
        provideHttpClient(withFetch()), // ou withXhr se preferir
      ],
    });
    service = TestBed.inject(UrlShortenerServiceService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
