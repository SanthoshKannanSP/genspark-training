import { ComponentFixture, TestBed } from '@angular/core/testing';

import { NewsManagementEdit } from './news-management-edit';

describe('NewsManagementEdit', () => {
  let component: NewsManagementEdit;
  let fixture: ComponentFixture<NewsManagementEdit>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [NewsManagementEdit]
    })
    .compileComponents();

    fixture = TestBed.createComponent(NewsManagementEdit);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
