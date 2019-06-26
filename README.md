# FileSystem
A simple file system implemented for the Clean Code course FMI 2019.

## Commands

* pwd - Prints the current working directory.
* ls - Lists a directory.
  * Syntax is ls \<directory\>
* cd - Changes the current directory.
  * Syntax is cd \<directory\>
* mkdir - Creates a directory.
  * Syntax is mkdir \<directory\>
* cat - Concatenas the content of files.
  * Syntax is cat \<file 1\> \<file 2\> ... \<file n\> > \<output file\>
  * If only output file is provided the input will be expected to be provided from the console and will be read until a single '.' is entered on a new line.
  * If no output file is provided the content of the concatenated files will be displayed in the console.
* rm - Remove the list of files.
  * Syntax is rm \<file 1\> \<file 2\> ... \<file n\>
* clear - Clears the console.
* exit - Exits the application.

## Example usage

```
$ pwd
/
$ mkdir folder
$ cat > file1
Test
.
$ ls
folder file1
$ cd folder
$ pwd
/folder
$ cd ..
$ rm file1
$ ls
folder
```
