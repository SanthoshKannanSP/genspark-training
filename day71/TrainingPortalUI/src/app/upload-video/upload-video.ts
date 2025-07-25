import { Component } from '@angular/core';
import { VideoService } from '../services/video.service';
import { Router, RouterLink } from '@angular/router';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-upload-video',
  imports: [RouterLink, FormsModule],
  templateUrl: './upload-video.html',
  styleUrl: './upload-video.css',
})
export class UploadVideo {
  title = '';
  description = '';
  selectedFile?: File;

  constructor(private videoService: VideoService, private router: Router) {}

  onFileChange(event: any) {
    this.selectedFile = event.target.files[0];
  }

  onSubmit() {
    if (!this.selectedFile || !this.title || !this.description) {
      alert('All fields are required.');
      return;
    }

    this.videoService
      .uploadVideo(this.title, this.description, this.selectedFile)
      .subscribe({
        next: () => {
          alert('Video uploaded successfully!');
          this.router.navigate(['/videos']);
        },
        error: (err) => {
          alert('Upload failed: ' + err.message);
          console.log(err);
        },
      });
  }
}
