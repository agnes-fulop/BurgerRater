import { Component, OnDestroy, OnInit } from '@angular/core';
import { FormBuilder, NgForm } from '@angular/forms';
import { MatOption } from '@angular/material/core';
import { MatSelectChange } from '@angular/material/select/select';

import { ActivatedRoute, Router } from '@angular/router';
import { takeUntil } from 'rxjs/internal/operators/takeUntil';
import { Subject } from 'rxjs/internal/Subject';
import { Subscription } from 'rxjs/internal/Subscription';
import { Review } from '../models/review';
import { ReviewService } from '../services/review.service';


@Component({
  selector: 'app-review-page',
  templateUrl: './review-page.component.html',
  styleUrls: ['./review-page.component.css']
})
export class ReviewPageComponent implements OnInit, OnDestroy  {

  reviews: Review[] = [];

  tasteScore = 0;
  textureScore = 0;
  visualPresentation = 0;
  displaySuccess = false;

  notifier = new Subject();

  private restaurantId = '';

  constructor(private route: ActivatedRoute, 
    private service: ReviewService, 
    private formBuilder: FormBuilder) { }

  ngOnInit(): void {
    this.route.paramMap
      .pipe(takeUntil(this.notifier))
      .subscribe((params) => {
      this.restaurantId = params.get('id')!;
      this.getReviews(this.restaurantId);
    })
    
  }

  getReviews(restaurantId: string): void {
    this.service.getReviewsForRestaurant(restaurantId)
      .pipe(takeUntil(this.notifier))
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

    this.service.postReview(this.restaurantId, review)
      .pipe(takeUntil(this.notifier))
      .subscribe(() => this.displaySuccess = true);
  }

  ngOnDestroy() {
    this.notifier.next()
    this.notifier.complete()
  }
}
