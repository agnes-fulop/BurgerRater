import { Component, OnInit } from '@angular/core';
import { NgForm } from '@angular/forms';
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

  constructor(private route: ActivatedRoute, private router: Router, private service: ReviewService) { }

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

  addReview(): void {
    let review = { tasteScore: this.tasteScore, textureScore: this.textureScore, visualPresentationScore: this.visualPresentation } as Review;

    this.service.postReview(this.restaurantId, review);
  }
}
