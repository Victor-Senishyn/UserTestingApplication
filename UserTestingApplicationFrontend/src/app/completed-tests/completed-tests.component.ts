import { CommonModule } from '@angular/common';
import { HttpClient, HttpClientModule } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { environment } from '../../environments/environment';
import { TokenContextService } from '../token-context.service';

@Component({
  selector: 'app-completed-tests',
  standalone: true,
  imports: [HttpClientModule, CommonModule],
  templateUrl: './completed-tests.component.html',
  styleUrl: './completed-tests.component.css',
})
export class CompletedTestsComponent implements OnInit {
  tests: any[] = [];

  constructor(
    private http: HttpClient,
    private tokenContext: TokenContextService
  ) {}

  ngOnInit(): void {
    this.getCompletedTests();
  }

  getCompletedTests(): void {
    const url = `${environment.apiUrl}/completed`;

    const accessToken = this.tokenContext.getAccessToken();
    const headers = { Authorization: `Bearer ${accessToken}` };

    this.http.get(url, { headers }).subscribe(
      (response: any) => {
        console.log('Available tests:', response);
        this.tests = response;
      },
      (error) => {
        console.error('Error getting completed tests:', error);
      }
    );
  }
}