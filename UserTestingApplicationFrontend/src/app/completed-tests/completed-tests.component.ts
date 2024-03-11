import { CommonModule } from '@angular/common';
import { HttpClient, HttpClientModule } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-completed-tests',
  standalone: true,
  imports: [HttpClientModule, CommonModule],
  templateUrl: './completed-tests.component.html',
  styleUrl: './completed-tests.component.css',
})
export class CompletedTestsComponent implements OnInit {
  tests: any[] = [];

  constructor(private http: HttpClient) {}

  ngOnInit(): void {
    this.getCompletedTests();
  }

  getCompletedTests(): void {
    const url = 'https://localhost:7212/completed';

    const accessToken = localStorage.getItem('accessToken');
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