import { Component, OnInit } from '@angular/core';
import { NewsService } from '../../../services/news-service';
import { Router, RouterLink } from '@angular/router';
import { NewsModel } from '../../../models/newsModel';
import { DatePipe } from '@angular/common';

@Component({
  selector: 'app-news-management',
  imports: [DatePipe, RouterLink],
  templateUrl: './news-management.html',
  styleUrl: './news-management.css',
})
export class NewsManagement implements OnInit {
  newsList: NewsModel[] = [];

  constructor(private newsService: NewsService, public router: Router) {}

  ngOnInit(): void {
    this.loadNews();
  }

  loadNews(): void {
    this.newsService.getAllNews().subscribe((data) => {
      this.newsList = data;
    });
  }

  exportToCSV(): void {
    this.newsService.exportToCSV();
  }

  exportToExcel(): void {
    this.newsService.exportToExcel();
  }

  deleteNews(id: number): void {
    if (confirm('Are you sure you want to delete this?')) {
      this.newsService.deleteNews(id).subscribe(() => this.loadNews());
    }
  }

  navigateToEdit(id: number): void {
    this.router.navigate(['/news/edit', id]);
  }

  navigateToDetails(id: number): void {
    this.router.navigate(['/news/details', id]);
  }
}
