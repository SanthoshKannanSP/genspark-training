import { ComponentFixture, TestBed } from '@angular/core/testing';

import { RecipeGrid } from './recipe-grid';
import { CUSTOM_ELEMENTS_SCHEMA } from '@angular/core';
import { RecipeService } from '../services/recipe-service';
import { of } from 'rxjs';
import { By } from '@angular/platform-browser';

describe('RecipeGrid', () => {
  let component: RecipeGrid;
  let fixture: ComponentFixture<RecipeGrid>;
  let recipeServiceSpy: jasmine.SpyObj<RecipeService>;

  const mockRecipes = {
    recipes: [
      {
        id: 1,
        name: 'Classic Margherita Pizza',
        cuisine: 'Italian',
        cookTimeMinutes: 15,
        ingredients: [
          'Pizza dough',
          'Tomato sauce',
          'Fresh mozzarella cheese',
          'Fresh basil leaves',
          'Olive oil',
          'Salt and pepper to taste',
        ],
        image: 'https://cdn.dummyjson.com/recipe-images/1.webp',
      },
      {
        id: 2,
        name: 'Vegetarian Stir-Fry',
        cuisine: 'Asian',
        cookTimeMinutes: 20,
        ingredients: [
          'Tofu, cubed',
          'Broccoli florets',
          'Carrots, sliced',
          'Bell peppers, sliced',
          'Soy sauce',
          'Ginger, minced',
          'Garlic, minced',
          'Sesame oil',
          'Cooked rice for serving',
        ],
        image: 'https://cdn.dummyjson.com/recipe-images/2.webp',
      },
      {
        id: 3,
        name: 'Chocolate Chip Cookies',
        cuisine: 'American',
        cookTimeMinutes: 10,
        ingredients: [
          'All-purpose flour',
          'Butter, softened',
          'Brown sugar',
          'White sugar',
          'Eggs',
          'Vanilla extract',
          'Baking soda',
          'Salt',
          'Chocolate chips',
        ],
        image: 'https://cdn.dummyjson.com/recipe-images/3.webp',
      },
    ],
    total: 50,
    skip: 0,
    limit: 3,
  };

  beforeEach(async () => {
    const spy = jasmine.createSpyObj('RecipeService', ['getRecipes']);
    await TestBed.configureTestingModule({
      imports: [RecipeGrid],
      providers: [{ provide: RecipeService, useValue: spy }],
      schemas: [CUSTOM_ELEMENTS_SCHEMA],
    }).compileComponents();

    recipeServiceSpy = TestBed.inject(
      RecipeService
    ) as jasmine.SpyObj<RecipeService>;
    recipeServiceSpy.getRecipes.and.returnValue(of(mockRecipes));

    fixture = TestBed.createComponent(RecipeGrid);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });

  it('should render recipe cards', () => {
    fixture.detectChanges();
    const cards = fixture.debugElement.queryAll(By.css('.card'));
    expect(cards.length).toBe(mockRecipes.recipes.length);
    console.log(cards[0].nativeElement);
    expect(
      cards[0].query(By.css('.card-title')).nativeElement.textContent
    ).toContain(mockRecipes.recipes[0].name);
  });
});
