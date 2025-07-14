import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';

interface UrlShortenerResponse {
  originalUrl: string;
  shortUrl: string;
}

@Injectable({
  providedIn: 'root',
})
export class UrlShortenerServiceService {
  constructor(private httpClient: HttpClient) {}

  createShortUrl(OriginalUrl: string) {
    return this.httpClient.post<UrlShortenerResponse>('/url-shortener', {
      OriginalUrl,
    });
  }
}
