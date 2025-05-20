//1) Design a C# console app that uses a jagged array to store data for Instagram posts by multiple users. Each user can have a different number of posts, 
//and each post stores a caption and number of likes.

//You have N users, and each user can have M posts (varies per user).

//Each post has:

//A caption(string)

//A number of likes (int)

//Store this in a jagged array, where each index represents one user's list of posts.

//Display all posts grouped by user.

//No file/database needed — console input/output only.

//Example output
//Enter number of users: 2

//User 1: How many posts? 2
//Enter caption for post 1: Sunset at beach
//Enter likes: 150
//Enter caption for post 2: Coffee time
//Enter likes: 89

//User 2: How many posts? 1
//Enter caption for post 1: Hiking adventure
//Enter likes: 230

//-- - Displaying Instagram Posts ---
//User 1:
//Post 1 - Caption: Sunset at beach | Likes: 150
//Post 2 - Caption: Coffee time | Likes: 89

//User 2:
//Post 1 - Caption: Hiking adventure | Likes: 230


//Test case
//| User | Number of Posts | Post Captions        | Likes      |
//| ---- | --------------- | -------------------- | ---------- |
//| 1    | 2               | "Lunch", "Road Trip" | 40, 120    |
//| 2    | 1               | "Workout"            | 75         |
//| 3    | 3               | "Book", "Tea", "Cat" | 30, 15, 60 |


Console.Write("Enter number of users: ");
int num_of_users;
while (!int.TryParse(Console.ReadLine(), out num_of_users))
    Console.WriteLine("Enter a valid number!");

Post[][] postsArray = new Post[num_of_users][];

int num_of_posts;
for(int user=0;user<num_of_users;user++)
{
    Console.WriteLine($"User {user+1}:");
    Console.WriteLine("How many posts? ");
    while (!int.TryParse(Console.ReadLine(), out num_of_posts))
        Console.WriteLine("Enter a valid number!");
    postsArray[user] = new Post[num_of_posts];
    for(int post=0; post<num_of_posts; post++)
    {
        var curr_post = CreatePost(post+1);
        postsArray[user][post] = curr_post;
        Console.WriteLine();
    }
}

DisplayPosts(postsArray);

void DisplayPosts(Post[][] postsArray)
{
    Console.WriteLine("--- Displaying Instagram Posts ---");
    Console.WriteLine("| User | Number of Posts | Post Captions                  | Likes      |");
    Console.WriteLine("| ---- | --------------- | ------------------------------ | ---------- |");
    var num_of_users = postsArray.Length;
    for(int user=0;user<num_of_users;user++)
    {
        var num_of_posts = postsArray[user].Length;
        var captionString = String.Join(",",postsArray[user].Select(post => post.Caption));
        var likeString = String.Join(",", postsArray[user].Select(post => post.Likes));

        Console.WriteLine($"| {user+1,-4} | {num_of_posts,-15} | {captionString,-30} | {likeString,-10} |");
    }
}

static Post CreatePost(int post)
{
    Console.Write($"Enter caption of post {post}: ");
    var caption = Console.ReadLine().Trim();
    Console.Write($"Enter likes of post {post}: ");
    int likes;
    while (!int.TryParse(Console.ReadLine(), out likes))
        Console.WriteLine("Enter a valid number!");
    return new Post(caption, likes);
}