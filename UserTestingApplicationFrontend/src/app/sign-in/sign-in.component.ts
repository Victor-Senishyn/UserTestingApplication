import { Component } from '@angular/core';
import { HttpClient, HttpClientModule } from '@angular/common/http';
import { FormsModule } from '@angular/forms';

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

  constructor(private http: HttpClient) {}

  login(): void {
    const url = 'https://localhost:7212/login';
    const body = { email: this.email, password: this.password };

    this.http.post(url, body).subscribe(
      (response: any) => {
        console.log('Login successful:', response);
        localStorage.setItem('accessToken', response.accessToken);
      },
      (error) => {
        console.error('Login error:', error);
      }
    );
  }
}
