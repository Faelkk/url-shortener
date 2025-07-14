import { Injectable } from '@angular/core';
import { environment } from '../../enviroment/enviroment';

@Injectable({
  providedIn: 'root',
})
export class EnvironmentService {
  readonly production = environment.production;
  readonly apiUrl = environment.apiUrl;
}
