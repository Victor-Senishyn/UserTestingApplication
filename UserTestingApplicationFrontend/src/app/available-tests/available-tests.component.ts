import { CommonModule } from '@angular/common';
import { HttpClient, HttpClientModule } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { environment } from '../../environments/environment';
import { TokenContextService } from '../token-context.service';

@Component({
  selector: 'app-available-tests',
  standalone: true,
  imports: [HttpClientModule, CommonModule],
  templateUrl: './available-tests.component.html',
  styleUrl: './available-tests.component.css',
})
export class AvailableTestsComponent implements OnInit {
  tests: any[] = [];

  constructor(
    private http: HttpClient,
    private router: Router,
    private tokenContext: TokenContextService
  ) {}

  ngOnInit(): void {
    this.getAvailableTests();
  }

  getAvailableTests(): void {
    const url = `${environment.apiUrl}/available`;

    const accessToken = this.tokenContext.getAccessToken();
    const headers = { Authorization: `Bearer ${accessToken}` };

    this.http.get(url, { headers }).subscribe(
      (response: any) => {
        console.log('Available tests:', response);
        this.tests = response;
      },
      (error) => {
        console.error('Error getting available tests:', error);
      }
    );
  }
  openTest(test: any): void {
    console.log('Open test:', test.id);
    this.router.navigate(['/test', test.id]);
  }
}