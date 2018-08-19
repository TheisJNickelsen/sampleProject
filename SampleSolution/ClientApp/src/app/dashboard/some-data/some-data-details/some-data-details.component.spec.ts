import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { SomeDataDetailsComponent } from './some-data-details.component';

describe('SomeDataDetailsComponent', () => {
  let component: SomeDataDetailsComponent;
  let fixture: ComponentFixture<SomeDataDetailsComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [SomeDataDetailsComponent]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(SomeDataDetailsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
