import { ComponentFixture, TestBed } from '@angular/core/testing';
import { RecipeCard } from './recipe-card';
import { Component, CUSTOM_ELEMENTS_SCHEMA, Input } from '@angular/core';
import { By } from '@angular/platform-browser';

describe('RecipeCard', () => {
  let component: RecipeCard;
  let fixture: ComponentFixture<RecipeCard>;

  const mockRecipe = {
    id: 1,
    name: 'Classic Margherita Pizza',
    cuisine: 'Italian',
    cookTimeMinutes: '15',
    ingredients: [
      'Pizza dough',
      'Tomato sauce',
      'Fresh mozzarella cheese',
      'Fresh basil leaves',
      'Olive oil',
      'Salt and pepper to taste',
    ],
    image: 'https://cdn.dummyjson.com/recipe-images/1.webp',
  };

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [RecipeCard],
      schemas: [CUSTOM_ELEMENTS_SCHEMA],
    }).compileComponents();

    fixture = TestBed.createComponent(RecipeCard);
    component = fixture.componentInstance;
    component.recipe = mockRecipe;
    fixture.detectChanges();
  });

  it('should create the component', () => {
    expect(component).toBeTruthy();
  });

  it('should display recipe name', () => {
    const name = fixture.debugElement.query(By.css('.card-title'));
    expect(name.nativeElement.textContent).toContain(mockRecipe.name);
  });

  it('should display first 4 ingredients', () => {
    const ingredients = fixture.debugElement.query(By.css('.ingredients'));
    const expected = mockRecipe.ingredients.slice(0, 4).join(' | ');
    expect(ingredients.nativeElement.textContent).toContain(expected);
  });

  it('should display cook time', () => {
    const cookTime = fixture.debugElement.query(By.css('.cook-time-minutes'));
    expect(cookTime.nativeElement.textContent).toContain(
      `${mockRecipe.cookTimeMinutes} Minutes`
    );
  });

  it('should display cuisine', () => {
    const cuisine = fixture.debugElement.query(By.css('.cuisine'));
    expect(cuisine.nativeElement.textContent).toContain(mockRecipe.cuisine);
  });

  it('should display image', () => {
    const image = fixture.debugElement.query(By.css('img'));
    expect(image.nativeElement.src).toContain(mockRecipe.image);
  });
});
