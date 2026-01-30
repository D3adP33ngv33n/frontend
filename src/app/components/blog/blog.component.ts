import Prism from '../../../../src/main';
import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ActivatedRoute } from '@angular/router';
import { MarkdownModule, MarkdownService } from 'ngx-markdown';

@Component({
	selector: 'app-blog',
	imports: [MarkdownModule, CommonModule],
	providers: [MarkdownService],
	templateUrl: './blog.component.html',
	styleUrl: './blog.component.scss'
})
export class BlogComponent {

	public post: string = '';

	constructor(private route: ActivatedRoute) {
		this.route.paramMap.subscribe(params => {
			this.post = params.get('post') || '';
		});
	}

	ngAfterViewChecked() {
		console.log('444444');
		
		Prism.highlightAll(true, () => {
			console.log('highlighted!');
		});
	}
}
