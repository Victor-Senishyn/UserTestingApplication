import { TestBed } from '@angular/core/testing';

import { TokenContextService } from './token-context.service';

describe('TokenContextService', () => {
  let service: TokenContextService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(TokenContextService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
