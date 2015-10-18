# Python2Path

Python2Path adds Python to the System PATH Environmental Variable.

It automatically removes all previous paths to Python, so it can also be used to update your PATH if you changed Python versions.

[![Download Python2Path](https://a.fsdn.com/con/app/sf-download-button)](https://sourceforge.net/projects/python2path/files/latest/download)


# Features
    * Automatic detection of Python installations
    * Automatic removal of previous Python installatiosn from PATH


# Instructions

1. Download and run the software.  You can get it [HERE](https://sourceforge.net/projects/python2path/files/latest/download).

2. Enter either a valid Python version or a valid path to a Python installation.
	* If your version is Python 2.7.10, you could enter one of the following and the path will automatically be detected.
		* Python 2.7.10
		* Python 2.7
		* Python 27
		* 2.7.10
		* 2.7
		* 27.
	* If you installed python at a custom location, such as C:/MySecretLocation/Python, then you would enter the following
		* C:/MySecretLocation/Python


# Important

If you have a version of Python installed at a __custom location__, its path will not automatically be detected.  If you do not want that version of Python in your PATH, you will have to remove it manually.


# License

The Python2Path uses the MIT license, available [here](./LICENSE.txt)