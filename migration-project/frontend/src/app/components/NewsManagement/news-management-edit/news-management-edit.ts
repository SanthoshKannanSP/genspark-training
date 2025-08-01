import { Component } from '@angular/core';
import {
  FormBuilder,
  FormGroup,
  ReactiveFormsModule,
  Validators,
} from '@angular/forms';
import { NewsService } from '../../../services/news-service';
import { ActivatedRoute, Router, RouterLink } from '@angular/router';
import { UserService } from '../../../services/user-service';
import { DatePipe } from '@angular/common';

@Component({
  selector: 'app-news-management-edit',
  imports: [ReactiveFormsModule, RouterLink],
  templateUrl: './news-management-edit.html',
  styleUrl: './news-management-edit.css',
})
export class NewsManagementEdit {
  newsForm!: FormGroup;
  users: any[] = [];
  selectedFile: File | null = null;
  newsId!: number;

  constructor(
    private fb: FormBuilder,
    private newsService: NewsService,
    private userService: UserService,
    private route: ActivatedRoute,
    private router: Router
  ) {}

  ngOnInit() {
    this.newsId = Number.parseInt(this.route.snapshot.paramMap.get('id')!);
    this.loadForm();
    this.getUsers();
    this.getNews();
  }

  loadForm() {
    this.newsForm = this.fb.group({
      userId: ['', Validators.required],
      title: ['', Validators.required],
      shortDescription: [''],
      content: [''],
      createdDate: ['', Validators.required],
      status: ['', Validators.required],
    });
  }

  getUsers() {
    this.userService.getUsers().subscribe((users) => {
      this.users = users;
    });
  }

  getNews() {
    this.newsService.getNewsById(this.newsId).subscribe((news) => {
      this.newsForm.patchValue(news);
      let date: any = new Date(news.createdDate);
      const offset = date.getTimezoneOffset();
      date = new Date(date.getTime() - offset * 60 * 1000);
      date = date.toISOString().split('T')[0];
      this.newsForm.controls['createdDate'].patchValue(date);
      console.log(this.newsForm.controls['createdDate'].value);
    });
  }

  onFileSelected(event: Event) {
    const file = (event.target as HTMLInputElement).files?.[0] ?? null;
    this.selectedFile = file;
  }

  onSubmit() {
    const formData = new FormData();
    formData.append('newsId', String(this.newsId));
    formData.append('userId', this.newsForm.get('userId')?.value);
    formData.append('title', this.newsForm.get('title')?.value);
    formData.append(
      'shortDescription',
      this.newsForm.get('shortDescription')?.value || ''
    );
    formData.append('content', this.newsForm.get('content')?.value || '');
    formData.append('createdDate', this.newsForm.get('createdDate')?.value);
    formData.append('status', this.newsForm.get('status')?.value);
    if (this.selectedFile) {
      formData.append('image', this.selectedFile, this.selectedFile.name);
    }
    this.newsService.updateNews(formData).subscribe(() => {
      this.router.navigate(['/news-management']);
    });
  }
}
