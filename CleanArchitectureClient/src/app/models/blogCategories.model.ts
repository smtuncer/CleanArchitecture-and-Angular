export class BlogCategoriesModel {
    id: string = "";
    blogCategoryImageUrl: string = "";
    categoryName: string = "";
    categoryDescription: string = "";
    createdDate: Date = new Date();
    updatedDate: Date = new Date();
    isPublished: boolean = true;
}