import { Component, OnInit } from '@angular/core';
import { NgForm } from '@angular/forms';

import { TodoService } from './../todo.service';
import { Todo } from '../todo';
import { Restaurant } from '../models/restaurant';

@Component({
  selector: 'app-todo-view',
  templateUrl: './todo-view.component.html',
  styleUrls: ['./todo-view.component.css']
})
export class TodoViewComponent implements OnInit {
  
  todo?: Todo;

  restaurants: Restaurant[] = [];

  displayedColumns = ['name', 'address'];
  //displayedColumns = ['name', 'address', 'edit', 'remove'];

  constructor(private service: TodoService) { }

  ngOnInit(): void {
    this.getTodos();
  }

  getTodos(): void {
    this.service.getTodos()
      .subscribe((todos: Restaurant[]) => {
        console.log(todos);

        this.restaurants = todos;
      });
  }

  addTodo(add: NgForm): void {
    this.service.postTodo(add.value).subscribe(() => {
      this.getTodos();
    })
    add.resetForm();
  }

  checkTodo(todo: Todo): void {
    this.service.editTodo(todo).subscribe();
  }

  removeTodo(id: string): void {
    this.service.deleteTodo(+id).subscribe(() => {
      this.getTodos();
    })
  }

}
