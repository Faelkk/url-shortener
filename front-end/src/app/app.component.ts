import { Component, signal } from '@angular/core';
import {
  FormControl,
  Validators,
  FormsModule,
  ReactiveFormsModule,
} from '@angular/forms';
import { MatInputModule } from '@angular/material/input';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatButtonModule } from '@angular/material/button';
import { UrlShortenerServiceService } from './url-shortener-service.service';
import { ToastrService } from 'ngx-toastr';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [
    FormsModule,
    ReactiveFormsModule,
    CommonModule,
    MatFormFieldModule,
    MatInputModule,
    MatButtonModule,
  ],
  templateUrl: './app.component.html',
  styleUrl: './app.component.scss',
})
export class AppComponent {
  title = 'front-end';
  constructor(
    private urlShortenerService: UrlShortenerServiceService,
    private toastService: ToastrService
  ) {}

  isLoading = signal(false);
  shortUrl = signal<string | null>(null);
  originalUrl = signal<string | null>(null);
  OriginalUrlForm = new FormControl('', [
    Validators.required,
    Validators.pattern(/^(https?|ftp):\/\/[^\s/$.?#].[^\s]*$/i),
  ]);

  submit() {
    if (this.OriginalUrlForm.invalid) return;

    this.isLoading.set(true);
    this.shortUrl.set(null);

    this.urlShortenerService
      .createShortUrl(this.OriginalUrlForm.value!)
      .subscribe({
        next: (response) => {
          console.log(response, 'response');
          this.toastService.success('Url encurtada com sucesso');
          this.isLoading.set(false);
          this.shortUrl.set(
            `http://localhost:5010/url-shortener/${response.shortUrl}`
          );
          this.originalUrl.set(`${response.originalUrl}`);
          this.OriginalUrlForm.reset();
        },
        error: () => {
          this.isLoading.set(false);
          this.toastService.error(
            'Erro inesperado! Tente novamente mais tarde.'
          );
        },
      });
  }
}
