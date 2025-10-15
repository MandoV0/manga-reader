## TODO
- ** MVP **
  - Email Verification
  - Password Reset/Change
  - User Profile Management (Delete User Profile)

  - Series Bookmarks
  - Searching
  - GET: All available Genres endpoint       DONE
  - Input Validation for all DTOs
  - Standardize DTOs 

  - Unit Tests


# Standardize DTOs
For the Folder structure inside the DTOs Folder do User/Auth/Series/Chapter Folders
## Client Input
Use this naming Convention:
Create*Dto
Update*Dto
*RequestDto

## Server Response
*Dto
*ListDto

# Password Reset/Change
We will need these Endpoints
- `POST /auth/forgot-password`
- `POST /auth/reset-password`
- `PUT /auth/change-password` [authenticated endpoint]

For forgot and reset password we simply send a email to the User.

# User Profile
We need to add cascade deletes so we delete all User data when a User wants to delete his account.

# Genre Controller
Create a Genre Controller.
It has only one endpoint which returns all Genres we have.
- `GET /genres`

# Unit Tests
Add Unit tests 