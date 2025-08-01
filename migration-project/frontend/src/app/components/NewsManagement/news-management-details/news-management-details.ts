import { Component, OnInit } from '@angular/core';
import { NewsModel } from '../../../models/newsModel';
import { ActivatedRoute, RouterLink } from '@angular/router';
import { NewsService } from '../../../services/news-service';
import { DatePipe } from '@angular/common';

@Component({
  selector: 'app-news-management-details',
  imports: [RouterLink, DatePipe],
  templateUrl: './news-management-details.html',
  styleUrl: './news-management-details.css',
})
export class NewsManagementDetails implements OnInit {
  news!: NewsModel;

  constructor(
    private route: ActivatedRoute,
    private newsService: NewsService
  ) {}

  ngOnInit(): void {
    const id = Number(this.route.snapshot.paramMap.get('id'));
    this.newsService.getNewsById(id).subscribe({
      next: (data) => (this.news = data),
      error: (err) => console.error('Error loading news', err),
    });
  }
}
