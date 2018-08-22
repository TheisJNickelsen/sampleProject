import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { UserSearchBoxComponentComponent } from './user-search-box-component.component';

describe('UserSearchBoxComponentComponent', () => {
  let component: UserSearchBoxComponentComponent;
  let fixture: ComponentFixture<UserSearchBoxComponentComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ UserSearchBoxComponentComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(UserSearchBoxComponentComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
