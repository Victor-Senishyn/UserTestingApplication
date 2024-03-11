import { CommonModule } from '@angular/common';
import { HttpClient, HttpClientModule } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { environment } from '../../environments/environment';
import { TokenContextService } from '../token-context.service';
import { Question } from './question.model';

@Component({
  selector: 'app-test',
  standalone: true,
  imports: [HttpClientModule, CommonModule],
  templateUrl: './test.component.html',
  styleUrls: ['./test.component.css'],
})
export class TestComponent implements OnInit {
  questions: Question[] = [];
  id!: number;

  constructor(
    private http: HttpClient,
    private route: ActivatedRoute,
    private router: Router,
    private tokenContext: TokenContextService
  ) {}

  ngOnInit(): void {
    this.id = Number(this.route.snapshot.paramMap.get('id'));
    this.getAvailableTests();
  }

  getAvailableTests(): void {
    const url = `${environment.apiUrl}/questions/${this.id}`;
    const accessToken = this.tokenContext.getAccessToken();
    const headers = { Authorization: `Bearer ${accessToken}` };

    this.http.get(url, { headers }).subscribe(
      (response: any) => {
        console.log('Test details:', response);
        this.questions = response;
      },
      (error) => {
        console.error('Error getting test:', error);
      }
    );
  }

  submitAnswers(): void {
    const selectedAnswers: {
      testId: number;
      questionIds: number[];
      selectedAnswerIds: number[];
    } = {
      testId: this.id,
      questionIds: [],
      selectedAnswerIds: [],
    };

    for (const question of this.questions) {
      const selectedAnswerId = document
        .querySelector(`input[name=answer_${question.id}]:checked`)
        ?.getAttribute('value');

      if (selectedAnswerId) {
        selectedAnswers.questionIds.push(question.id);
        selectedAnswers.selectedAnswerIds.push(parseInt(selectedAnswerId, 10));
      }
    }

    const url = `${environment.apiUrl}/api/tests`;
    const accessToken = localStorage.getItem('accessToken');
    const headers = { Authorization: `Bearer ${accessToken}` };

    this.http.patch(url, selectedAnswers, { headers }).subscribe(
      (response: any) => {
        this.questions = response;
        const score = response.score;
        alert(`Score : ${score}`);
      },
      (error) => {
        console.error('Error submitting test:', error);
      }
    );

    console.log('Selected answers:', selectedAnswers);
    this.router.navigate(['/available-tests']);
  }
}
