import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule, RouterOutlet } from '@angular/router';
import { AvailableTestsComponent } from './available-tests/available-tests.component';
import { CompletedTestsComponent } from './completed-tests/completed-tests.component';
import { SignInComponent } from './sign-in/sign-in.component';
import { SignUpComponent } from './sign-up/sign-up.component';
import { TestComponent } from './test/test.component';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [CommonModule, RouterOutlet, RouterModule, AvailableTestsComponent, 
    CompletedTestsComponent, SignInComponent, SignUpComponent, TestComponent],
  templateUrl: './app.component.html',
  styleUrl: './app.component.css',
})
export class AppComponent {
  title = 'my-project';
  links = [
    {
      path: '/available-tests',
      label: 'available-tests',
      active: 'button-active',
    },
    {
      path: '/completed-tests',
      label: 'completed-tests',
      active: 'button-active',
    },
  ];
}

