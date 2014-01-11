mersenne.fs
-----------

A Mersenne Twister implementation in Forth


Getting Started
---------------

I've only tested this in gforth and have not proved it is without bugs.  Do not use for security purposes.

To add to your gforth project

	s" mersenne.fs" included
	genrand .

It currently clamps to 32bits, and has a 623 element matrix.


About
-----

This generator is based on the out of date C reference found here:

http://www.math.sci.hiroshima-u.ac.jp/~m-mat/MT/VERSIONS/C-LANG/991029/mt19937int.c

However, this will be good enough for most PRNG use on a PC.


Complaints & Bug Fixed
----------------------

Please send any complaints or patches to:

	David Goehrig <dave@dloh.org>
