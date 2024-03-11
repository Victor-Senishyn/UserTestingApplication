import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root',
})
export class TokenContextService {
  private readonly accessTokenKey = 'accessToken';

  setAccessToken(token: string): void {
    localStorage.setItem(this.accessTokenKey, token);
  }

  getAccessToken(): string | null {
    return localStorage.getItem(this.accessTokenKey);
  }
}
