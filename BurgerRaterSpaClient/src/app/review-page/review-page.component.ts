import { Component, OnInit } from '@angular/core';
import { FormBuilder, NgForm } from '@angular/forms';
import { MatOption } from '@angular/material/core';
import { MatSelectChange } from '@angular/material/select/select';

import { ActivatedRoute, Router } from '@angular/router';
import { Review } from '../models/review';
import { ReviewService } from '../services/review.service';


@Component({
  selector: 'app-review-page',
  templateUrl: './review-page.component.html',
  styleUrls: ['./review-page.component.css']
})
export class ReviewPageComponent implements OnInit {

  reviews: Review[] = [];

  tasteScore = 0;
  textureScore = 0;
  visualPresentation = 0;

  private restaurantId = '';

  constructor(private route: ActivatedRoute, 
    private service: ReviewService, 
    private formBuilder: FormBuilder) { }

  ngOnInit(): void {
    this.route.paramMap.subscribe((params) => {
      this.restaurantId = params.get('id')!;
      this.getReviews(this.restaurantId);
    })
    
  }

  getReviews(restaurantId: string): void {
    this.service.getReviewsForRestaurant(restaurantId)
      .subscribe((reviews: Review[]) => {
        this.reviews = reviews;
      });
  }

  selectedTaste(event: MatSelectChange) {
    const selectedData = {
      text: (event.source.selected as MatOption).viewValue,
      value: event.source.value
    };

    this.tasteScore = selectedData.value;
    console.log(selectedData);
  }

  selectedTexture(event: MatSelectChange) {
    const selectedData = {
      text: (event.source.selected as MatOption).viewValue,
      value: event.source.value
    };

    this.textureScore = selectedData.value;
    console.log(selectedData);
  }

  selectedvisualPresentation(event: MatSelectChange) {
    const selectedData = {
      text: (event.source.selected as MatOption).viewValue,
      value: event.source.value
    };

    this.visualPresentation = selectedData.value;
    console.log(selectedData);
  }

  onSubmit(): void {
    let review = { tasteScore: this.tasteScore, textureScore: this.textureScore, visualPresentationScore: this.visualPresentation } as Review;

    this.service.postReview(this.restaurantId, review).subscribe();
  }
}
