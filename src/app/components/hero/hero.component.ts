import { Component } from '@angular/core';

@Component({
  selector: 'app-hero',
  imports: [],
  templateUrl: './hero.component.html',
  styleUrl: './hero.component.scss'
})
export class HeroComponent {
  typingText = 'PROTECTION BEYOND PERIMETERS';
  typedText = '';
  typingIndex = 0;
  typingSpeed = 100;
  
  ngOnInit() {
    this.typeText();
  }
  
  typeText() {
    if (this.typingIndex < this.typingText.length) {
      this.typedText += this.typingText.charAt(this.typingIndex);
      this.typingIndex++;
      setTimeout(() => this.typeText(), this.typingSpeed);
    } else {
      // Restart after delay
      setTimeout(() => {
        this.typedText = '';
        this.typingIndex = 0;
        this.typeText();
      }, 3000);
    }
  }

}
