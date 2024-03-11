import { Routes } from '@angular/router';
import { AvailableTestsComponent } from './available-tests/available-tests.component';
import { CompletedTestsComponent } from './completed-tests/completed-tests.component';
import { SignInComponent } from './sign-in/sign-in.component';
import { SignUpComponent } from './sign-up/sign-up.component';
import { TestComponent } from './test/test.component';
import { TestResultComponent } from './test-result/test-result.component';

export const routes: Routes = [
  { path: '', pathMatch: 'full', redirectTo: 'main' },
  { path: 'available-tests', component: AvailableTestsComponent },
  { path: 'completed-tests', component: CompletedTestsComponent },
  { path: 'sign-in', component: SignInComponent },
  { path: 'sign-up', component: SignUpComponent },
  { path: 'test/:id', component: TestComponent },
  { path: 'test-result', component: TestResultComponent },
];
