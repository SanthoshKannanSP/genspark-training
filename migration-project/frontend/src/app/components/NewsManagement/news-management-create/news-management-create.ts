import { Component, OnInit } from '@angular/core';
import {
  FormBuilder,
  FormGroup,
  ReactiveFormsModule,
  Validators,
} from '@angular/forms';
import { NewsService } from '../../../services/news-service';
import { Router, RouterLink } from '@angular/router';
import { UserService } from '../../../services/user-service';
import { User } from '../../../models/user';

@Component({
  selector: 'app-news-management-create',
  imports: [ReactiveFormsModule, RouterLink],
  templateUrl: './news-management-create.html',
  styleUrl: './news-management-create.css',
})
export class NewsManagementCreate implements OnInit {
  newsForm!: FormGroup;
  users: User[] = [];
  selectedFile: File | null = null;

  onFileSelected(event: any): void {
    if (event.target.files && event.target.files.length > 0) {
      this.selectedFile = event.target.files[0];
    }
  }

  constructor(
    private fb: FormBuilder,
    private newsService: NewsService,
    private userService: UserService,
    private router: Router
  ) {}

  ngOnInit(): void {
    this.initForm();
    this.loadUsers();
  }

  initForm(): void {
    this.newsForm = this.fb.group({
      userId: ['', Validators.required],
      title: ['', Validators.required],
      shortDescription: [''],
      content: [''],
      createdDate: [
        new Date().toISOString().substring(0, 10),
        Validators.required,
      ],
      status: ['1', Validators.required],
    });
  }

  loadUsers(): void {
    this.userService.getUsers().subscribe((data) => {
      this.users = data;
    });
  }

  onSubmit(): void {
    if (this.newsForm.valid && this.selectedFile) {
      const formData = new FormData();

      formData.append('userId', this.newsForm.get('userId')?.value);
      formData.append('title', this.newsForm.get('title')?.value);
      formData.append(
        'shortDescription',
        this.newsForm.get('shortDescription')?.value || ''
      );
      formData.append('content', this.newsForm.get('content')?.value || '');
      formData.append('createdDate', this.newsForm.get('createdDate')?.value);
      formData.append('status', this.newsForm.get('status')?.value);
      formData.append('image', this.selectedFile!, this.selectedFile!.name);

      this.newsService.createNews(formData).subscribe(() => {
        this.router.navigate(['/news-management']);
      });
    }
  }
}
