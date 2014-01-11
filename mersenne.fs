\ mersenne.fs
\ copyright © 2013 David J. Goehrig

624 constant N
397 constant M
$9908b0df constant MATRIX_A
$80000000 constant UPPER_MASK
$7fffffff constant LOWER_MASK

$9d2c5680 constant TEMPERING_MASK_B
$efc60000 constant TEMPERING_MASK_C

: TEMPERING_SHIFT_U 11 rshift ;
: TEMPERING_SHIFT_S 7 lshift ;
: TEMPERING_SHIFT_T 15 lshift ;
: TEMPERING_SHIFT_L 18 rshift ;

create mt N cells allot		\ a state vector
N 1 + value mti			\ mti > N => not initialized

: sgenrand ( seed -- )
	$ffffffff and mt !
	N 1 do mt I cells + 69069 over 1 cells - @ * $ffffffff and swap ! loop
;

0 value Y
create mag 2 cells allot

: prep
	mti n 1 + = if 4357 sgenrand then
;

: mtwist
	N M - 0 do 
		mt I cells + UPPER_MASK over @ and	
		over 1 cells + @ LOWER_MASK and or to y
		M N - over + y 1 rshift xor y 1 and cells mag + @ xor
		swap !
	loop
;

: m-ntwist 
	N 1 - N M - do
		mt I cells + UPPER_MASK over @ and over 1 cells + @ LOWER_MASK and or to Y
		M N - cells over + @ Y 1 rshift xor Y 1 and cells mag + @ xor
		swap !
	loop
;

: n-1twist
	mt n 1 - cells + @ UPPER_MASK and mt @ LOWER_MASK and or to y
	mt m 1 - cells + @ y 1 rshift xor y 1 and cells mag + @ xor
	mt n 1 - cells + !
	0 to mti
;
	

: genrand ( -- )
	0 mag ! MATRIX_A mag 1 cells + !
	mti N >= if
		prep
		mtwist
		m-ntwist
		n-1twist
	then
	mt mti cells + @
	mti 1 + to mti
	dup TEMPERING_SHIFT_U xor
	dup TEMPERING_SHIFT_S TEMPERING_MASK_B and xor
	dup TEMPERING_SHIFT_T TEMPERING_MASK_C and xor
	dup TEMPERING_SHIFT_L xor	
;


