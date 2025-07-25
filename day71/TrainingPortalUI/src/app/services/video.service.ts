import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Video } from '../models/video.model';

@Injectable()
export class VideoService {
  private baseUrl = 'http://localhost:5083/api/Video';

  constructor(private http: HttpClient) {}

  getAllVideos(): Observable<Video[]> {
    return this.http.get<Video[]>(this.baseUrl);
  }

  getVideoDetails(fileName: string): Observable<Video> {
    return this.http.get<Video>(`${this.baseUrl}/${fileName}/details`);
  }

  getVideoStreamUrl(fileName: string): string {
    return `${this.baseUrl}/${fileName}/stream`;
  }

  uploadVideo(title: string, description: string, file: File): Observable<any> {
    const formData = new FormData();
    formData.append('title', title);
    formData.append('description', description);
    formData.append('file', file);

    return this.http.post(`${this.baseUrl}/upload`, formData);
  }
}
