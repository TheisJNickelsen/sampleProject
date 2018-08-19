import { TestBed, inject } from '@angular/core/testing';
import { SomeDataService } from "./some-data.service";

describe('MySampleSolutionService', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [SomeDataService]
    });
  });

  it('should be created', inject([SomeDataService], (service: SomeDataService) => {
    expect(service).toBeTruthy();
  }));
});
