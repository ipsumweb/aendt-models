import { TestBed } from '@angular/core/testing';

import { GrowlerService } from './growler.service';

describe('GrowlerService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: GrowlerService = TestBed.get(GrowlerService);
    expect(service).toBeTruthy();
  });
});
