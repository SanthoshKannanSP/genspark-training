import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router, RouterLink } from '@angular/router';
import { NewsModel } from '../../../models/newsModel';
import { NewsService } from '../../../services/news-service';

@Component({
  selector: 'app-news',
  imports: [RouterLink],
  templateUrl: './news.html',
  styleUrl: './news.css',
})
export class News implements OnInit {
  newsList: NewsModel[] = [];
  pageNumber = 1;
  totalPages = 0;
  baseUrl = 'http://localhost:5288/api/News/Image/';

  constructor(
    private newsService: NewsService,
    private router: Router,
    private route: ActivatedRoute
  ) {
    this.route.queryParams.subscribe((params) => {
      this.pageNumber = params['page'] || 1;
      this.loadNews();
    });
  }

  ngOnInit(): void {
    this.loadNews();
  }

  loadNews(): void {
    this.newsService.getNews(this.pageNumber).subscribe((response) => {
      this.newsList = response.items;
      this.totalPages = response.totalPage;
    });
  }

  goToPage(page: number): void {
    this.router.navigate([], {
      relativeTo: this.route,
      queryParams: {
        page: page,
      },
      queryParamsHandling: 'merge',
    });
  }
}
