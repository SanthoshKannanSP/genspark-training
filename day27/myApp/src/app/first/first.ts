import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-first',
  imports: [FormsModule],
  templateUrl: './first.html',
  styleUrl: './first.css'
})
export class First {
  name:string;
  currentVal:string = "Empty";
  heart:string = "bi bi-balloon-heart"
  heartClicked:boolean = false;
  constructor()
  {
    this.name = "Ramu";
  }

  OnButtonClick(uname:string) {
    this.name = uname;
  }

  toggleHeart()
  {
    if (this.heartClicked)
    {
      this.heart = "bi bi-balloon-heart";
    }
    else
    {
      this.heart = "bi bi-balloon-heart-fill"
    }
    this.heartClicked = !this.heartClicked;
  }

}
