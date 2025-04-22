# Syngenta---CheckWeight---QC


CREATE TABLE IF NOT EXISTS public."Roles" (
    "Id" SERIAL PRIMARY KEY,             -- Cột Id tự động tăng, khớp với kiểu int trong C#
    "Name" TEXT NOT NULL,                 -- Cột Name, kiểu TEXT (không thể null)
    "Description" TEXT,                   -- Cột Description, kiểu TEXT (có thể null)
    "Permission" TEXT,                    -- Cột Permission, kiểu TEXT (có thể null)
    "Passwords" TEXT NOT NULL,            -- Cột Passwords, kiểu TEXT (không thể null)
    "isEnable" BOOLEAN DEFAULT false,     -- Cột isEnable, kiểu BOOLEAN (mặc định là false)
    "CreatedAt" TIMESTAMP DEFAULT CURRENT_TIMESTAMP,  -- Cột CreatedAt, kiểu TIMESTAMP (mặc định là thời gian hiện tại)
    "UpdatedAt" TIMESTAMP DEFAULT CURRENT_TIMESTAMP   -- Cột UpdatedAt, kiểu TIMESTAMP (mặc định là thời gian hiện tại)
);
