import { ComponentFixture, TestBed } from '@angular/core/testing';

import { NewsManagementCreate } from './news-management-create';

describe('NewsManagementCreate', () => {
  let component: NewsManagementCreate;
  let fixture: ComponentFixture<NewsManagementCreate>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [NewsManagementCreate]
    })
    .compileComponents();

    fixture = TestBed.createComponent(NewsManagementCreate);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
