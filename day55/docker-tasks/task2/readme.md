# Task-2: Use Docker Volumes

## Task:

- Run a MySQL container with a named volume to persist data.

## Steps:

- Pull the mysql:latest image.
- Run with -v mydbdata:/var/lib/mysql.
- Connect via mysql client.
- Insert some data.
- Stop and remove the container.
- Start again and verify the data is still there.
