import { Component } from '@angular/core';
import { HttpClient, HttpClientModule } from '@angular/common/http';
import { FormsModule } from '@angular/forms';
import { environment } from '../../environments/environment';
import { TokenContextService } from '../token-context.service';

@Component({
  selector: 'app-sign-in',
  standalone: true,
  imports: [FormsModule, HttpClientModule],
  templateUrl: './sign-in.component.html',
  styleUrl: './sign-in.component.css',
})
export class SignInComponent {
  email!: string;
  password!: string;

  constructor(
    private http: HttpClient,
    private tokenContext: TokenContextService
  ) {}

  login(): void {
    const url = `${environment.apiUrl}/login`;
    const body = { email: this.email, password: this.password };

    this.http.post(url, body).subscribe(
      (response: any) => {
        console.log('Login successful:', response);
        this.tokenContext.setAccessToken(response.accessToken);
      },
      (error) => {
        console.error('Login error:', error);
      }
    );
  }
}
