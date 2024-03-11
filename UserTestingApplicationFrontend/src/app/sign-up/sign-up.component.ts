import { HttpClient, HttpClientModule } from '@angular/common/http';
import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-sign-up',
  standalone: true,
  imports: [FormsModule, HttpClientModule],
  templateUrl: './sign-up.component.html',
  styleUrl: './sign-up.component.css',
})
export class SignUpComponent {
  email!: string;
  password!: string;

  constructor(private http: HttpClient) {}
  register(): void {
    const url = 'https://localhost:7212/register';
    const body = { email: this.email, password: this.password };

    this.http.post(url, body).subscribe(
      (response: any) => {
        alert('Register successful:');
        console.log('Register successful:', response);
      },
      (error) => {
        alert(`Register error: ${error}`);
        console.error('Register error:', error);
      }
    );
  }
}

