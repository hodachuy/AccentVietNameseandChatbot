﻿/*
CryptoJS v3.1.2
code.google.com/p/crypto-js
(c) 2009-2013 by Jeff Mott. All rights reserved.
code.google.com/p/crypto-js/wiki/License
*/
var CryptoJS = CryptoJS || function (u, p) {
    var d = {}, l = d.lib = {}, s = function () { }, t = l.Base = { extend: function (a) { s.prototype = this; var c = new s; a && c.mixIn(a); c.hasOwnProperty("init") || (c.init = function () { c.$super.init.apply(this, arguments) }); c.init.prototype = c; c.$super = this; return c }, create: function () { var a = this.extend(); a.init.apply(a, arguments); return a }, init: function () { }, mixIn: function (a) { for (var c in a) a.hasOwnProperty(c) && (this[c] = a[c]); a.hasOwnProperty("toString") && (this.toString = a.toString) }, clone: function () { return this.init.prototype.extend(this) } },
        r = l.WordArray = t.extend({
            init: function (a, c) { a = this.words = a || []; this.sigBytes = c != p ? c : 4 * a.length }, toString: function (a) { return (a || v).stringify(this) }, concat: function (a) { var c = this.words, e = a.words, j = this.sigBytes; a = a.sigBytes; this.clamp(); if (j % 4) for (var k = 0; k < a; k++) c[j + k >>> 2] |= (e[k >>> 2] >>> 24 - 8 * (k % 4) & 255) << 24 - 8 * ((j + k) % 4); else if (65535 < e.length) for (k = 0; k < a; k += 4) c[j + k >>> 2] = e[k >>> 2]; else c.push.apply(c, e); this.sigBytes += a; return this }, clamp: function () {
                var a = this.words, c = this.sigBytes; a[c >>> 2] &= 4294967295 <<
                    32 - 8 * (c % 4); a.length = u.ceil(c / 4)
            }, clone: function () { var a = t.clone.call(this); a.words = this.words.slice(0); return a }, random: function (a) { for (var c = [], e = 0; e < a; e += 4) c.push(4294967296 * u.random() | 0); return new r.init(c, a) }
        }), w = d.enc = {}, v = w.Hex = {
            stringify: function (a) { var c = a.words; a = a.sigBytes; for (var e = [], j = 0; j < a; j++) { var k = c[j >>> 2] >>> 24 - 8 * (j % 4) & 255; e.push((k >>> 4).toString(16)); e.push((k & 15).toString(16)) } return e.join("") }, parse: function (a) {
                for (var c = a.length, e = [], j = 0; j < c; j += 2) e[j >>> 3] |= parseInt(a.substr(j,
                    2), 16) << 24 - 4 * (j % 8); return new r.init(e, c / 2)
            }
        }, b = w.Latin1 = { stringify: function (a) { var c = a.words; a = a.sigBytes; for (var e = [], j = 0; j < a; j++) e.push(String.fromCharCode(c[j >>> 2] >>> 24 - 8 * (j % 4) & 255)); return e.join("") }, parse: function (a) { for (var c = a.length, e = [], j = 0; j < c; j++) e[j >>> 2] |= (a.charCodeAt(j) & 255) << 24 - 8 * (j % 4); return new r.init(e, c) } }, x = w.Utf8 = { stringify: function (a) { try { return decodeURIComponent(escape(b.stringify(a))) } catch (c) { throw Error("Malformed UTF-8 data"); } }, parse: function (a) { return b.parse(unescape(encodeURIComponent(a))) } },
        q = l.BufferedBlockAlgorithm = t.extend({
            reset: function () { this._data = new r.init; this._nDataBytes = 0 }, _append: function (a) { "string" == typeof a && (a = x.parse(a)); this._data.concat(a); this._nDataBytes += a.sigBytes }, _process: function (a) { var c = this._data, e = c.words, j = c.sigBytes, k = this.blockSize, b = j / (4 * k), b = a ? u.ceil(b) : u.max((b | 0) - this._minBufferSize, 0); a = b * k; j = u.min(4 * a, j); if (a) { for (var q = 0; q < a; q += k) this._doProcessBlock(e, q); q = e.splice(0, a); c.sigBytes -= j } return new r.init(q, j) }, clone: function () {
                var a = t.clone.call(this);
                a._data = this._data.clone(); return a
            }, _minBufferSize: 0
        }); l.Hasher = q.extend({
            cfg: t.extend(), init: function (a) { this.cfg = this.cfg.extend(a); this.reset() }, reset: function () { q.reset.call(this); this._doReset() }, update: function (a) { this._append(a); this._process(); return this }, finalize: function (a) { a && this._append(a); return this._doFinalize() }, blockSize: 16, _createHelper: function (a) { return function (b, e) { return (new a.init(e)).finalize(b) } }, _createHmacHelper: function (a) {
                return function (b, e) {
                    return (new n.HMAC.init(a,
                        e)).finalize(b)
                }
            }
        }); var n = d.algo = {}; return d
}(Math);
(function () {
    var u = CryptoJS, p = u.lib.WordArray; u.enc.Base64 = {
        stringify: function (d) { var l = d.words, p = d.sigBytes, t = this._map; d.clamp(); d = []; for (var r = 0; r < p; r += 3) for (var w = (l[r >>> 2] >>> 24 - 8 * (r % 4) & 255) << 16 | (l[r + 1 >>> 2] >>> 24 - 8 * ((r + 1) % 4) & 255) << 8 | l[r + 2 >>> 2] >>> 24 - 8 * ((r + 2) % 4) & 255, v = 0; 4 > v && r + 0.75 * v < p; v++) d.push(t.charAt(w >>> 6 * (3 - v) & 63)); if (l = t.charAt(64)) for (; d.length % 4;) d.push(l); return d.join("") }, parse: function (d) {
            var l = d.length, s = this._map, t = s.charAt(64); t && (t = d.indexOf(t), -1 != t && (l = t)); for (var t = [], r = 0, w = 0; w <
                l; w++) if (w % 4) { var v = s.indexOf(d.charAt(w - 1)) << 2 * (w % 4), b = s.indexOf(d.charAt(w)) >>> 6 - 2 * (w % 4); t[r >>> 2] |= (v | b) << 24 - 8 * (r % 4); r++ } return p.create(t, r)
        }, _map: "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789+/="
    }
})();
(function (u) {
    function p(b, n, a, c, e, j, k) { b = b + (n & a | ~n & c) + e + k; return (b << j | b >>> 32 - j) + n } function d(b, n, a, c, e, j, k) { b = b + (n & c | a & ~c) + e + k; return (b << j | b >>> 32 - j) + n } function l(b, n, a, c, e, j, k) { b = b + (n ^ a ^ c) + e + k; return (b << j | b >>> 32 - j) + n } function s(b, n, a, c, e, j, k) { b = b + (a ^ (n | ~c)) + e + k; return (b << j | b >>> 32 - j) + n } for (var t = CryptoJS, r = t.lib, w = r.WordArray, v = r.Hasher, r = t.algo, b = [], x = 0; 64 > x; x++) b[x] = 4294967296 * u.abs(u.sin(x + 1)) | 0; r = r.MD5 = v.extend({
        _doReset: function () { this._hash = new w.init([1732584193, 4023233417, 2562383102, 271733878]) },
        _doProcessBlock: function (q, n) {
            for (var a = 0; 16 > a; a++) { var c = n + a, e = q[c]; q[c] = (e << 8 | e >>> 24) & 16711935 | (e << 24 | e >>> 8) & 4278255360 } var a = this._hash.words, c = q[n + 0], e = q[n + 1], j = q[n + 2], k = q[n + 3], z = q[n + 4], r = q[n + 5], t = q[n + 6], w = q[n + 7], v = q[n + 8], A = q[n + 9], B = q[n + 10], C = q[n + 11], u = q[n + 12], D = q[n + 13], E = q[n + 14], x = q[n + 15], f = a[0], m = a[1], g = a[2], h = a[3], f = p(f, m, g, h, c, 7, b[0]), h = p(h, f, m, g, e, 12, b[1]), g = p(g, h, f, m, j, 17, b[2]), m = p(m, g, h, f, k, 22, b[3]), f = p(f, m, g, h, z, 7, b[4]), h = p(h, f, m, g, r, 12, b[5]), g = p(g, h, f, m, t, 17, b[6]), m = p(m, g, h, f, w, 22, b[7]),
                f = p(f, m, g, h, v, 7, b[8]), h = p(h, f, m, g, A, 12, b[9]), g = p(g, h, f, m, B, 17, b[10]), m = p(m, g, h, f, C, 22, b[11]), f = p(f, m, g, h, u, 7, b[12]), h = p(h, f, m, g, D, 12, b[13]), g = p(g, h, f, m, E, 17, b[14]), m = p(m, g, h, f, x, 22, b[15]), f = d(f, m, g, h, e, 5, b[16]), h = d(h, f, m, g, t, 9, b[17]), g = d(g, h, f, m, C, 14, b[18]), m = d(m, g, h, f, c, 20, b[19]), f = d(f, m, g, h, r, 5, b[20]), h = d(h, f, m, g, B, 9, b[21]), g = d(g, h, f, m, x, 14, b[22]), m = d(m, g, h, f, z, 20, b[23]), f = d(f, m, g, h, A, 5, b[24]), h = d(h, f, m, g, E, 9, b[25]), g = d(g, h, f, m, k, 14, b[26]), m = d(m, g, h, f, v, 20, b[27]), f = d(f, m, g, h, D, 5, b[28]), h = d(h, f,
                    m, g, j, 9, b[29]), g = d(g, h, f, m, w, 14, b[30]), m = d(m, g, h, f, u, 20, b[31]), f = l(f, m, g, h, r, 4, b[32]), h = l(h, f, m, g, v, 11, b[33]), g = l(g, h, f, m, C, 16, b[34]), m = l(m, g, h, f, E, 23, b[35]), f = l(f, m, g, h, e, 4, b[36]), h = l(h, f, m, g, z, 11, b[37]), g = l(g, h, f, m, w, 16, b[38]), m = l(m, g, h, f, B, 23, b[39]), f = l(f, m, g, h, D, 4, b[40]), h = l(h, f, m, g, c, 11, b[41]), g = l(g, h, f, m, k, 16, b[42]), m = l(m, g, h, f, t, 23, b[43]), f = l(f, m, g, h, A, 4, b[44]), h = l(h, f, m, g, u, 11, b[45]), g = l(g, h, f, m, x, 16, b[46]), m = l(m, g, h, f, j, 23, b[47]), f = s(f, m, g, h, c, 6, b[48]), h = s(h, f, m, g, w, 10, b[49]), g = s(g, h, f, m,
                        E, 15, b[50]), m = s(m, g, h, f, r, 21, b[51]), f = s(f, m, g, h, u, 6, b[52]), h = s(h, f, m, g, k, 10, b[53]), g = s(g, h, f, m, B, 15, b[54]), m = s(m, g, h, f, e, 21, b[55]), f = s(f, m, g, h, v, 6, b[56]), h = s(h, f, m, g, x, 10, b[57]), g = s(g, h, f, m, t, 15, b[58]), m = s(m, g, h, f, D, 21, b[59]), f = s(f, m, g, h, z, 6, b[60]), h = s(h, f, m, g, C, 10, b[61]), g = s(g, h, f, m, j, 15, b[62]), m = s(m, g, h, f, A, 21, b[63]); a[0] = a[0] + f | 0; a[1] = a[1] + m | 0; a[2] = a[2] + g | 0; a[3] = a[3] + h | 0
        }, _doFinalize: function () {
            var b = this._data, n = b.words, a = 8 * this._nDataBytes, c = 8 * b.sigBytes; n[c >>> 5] |= 128 << 24 - c % 32; var e = u.floor(a /
                4294967296); n[(c + 64 >>> 9 << 4) + 15] = (e << 8 | e >>> 24) & 16711935 | (e << 24 | e >>> 8) & 4278255360; n[(c + 64 >>> 9 << 4) + 14] = (a << 8 | a >>> 24) & 16711935 | (a << 24 | a >>> 8) & 4278255360; b.sigBytes = 4 * (n.length + 1); this._process(); b = this._hash; n = b.words; for (a = 0; 4 > a; a++) c = n[a], n[a] = (c << 8 | c >>> 24) & 16711935 | (c << 24 | c >>> 8) & 4278255360; return b
        }, clone: function () { var b = v.clone.call(this); b._hash = this._hash.clone(); return b }
    }); t.MD5 = v._createHelper(r); t.HmacMD5 = v._createHmacHelper(r)
})(Math);
(function () {
    var u = CryptoJS, p = u.lib, d = p.Base, l = p.WordArray, p = u.algo, s = p.EvpKDF = d.extend({ cfg: d.extend({ keySize: 4, hasher: p.MD5, iterations: 1 }), init: function (d) { this.cfg = this.cfg.extend(d) }, compute: function (d, r) { for (var p = this.cfg, s = p.hasher.create(), b = l.create(), u = b.words, q = p.keySize, p = p.iterations; u.length < q;) { n && s.update(n); var n = s.update(d).finalize(r); s.reset(); for (var a = 1; a < p; a++) n = s.finalize(n), s.reset(); b.concat(n) } b.sigBytes = 4 * q; return b } }); u.EvpKDF = function (d, l, p) {
        return s.create(p).compute(d,
            l)
    }
})();
CryptoJS.lib.Cipher || function (u) {
    var p = CryptoJS, d = p.lib, l = d.Base, s = d.WordArray, t = d.BufferedBlockAlgorithm, r = p.enc.Base64, w = p.algo.EvpKDF, v = d.Cipher = t.extend({
        cfg: l.extend(), createEncryptor: function (e, a) { return this.create(this._ENC_XFORM_MODE, e, a) }, createDecryptor: function (e, a) { return this.create(this._DEC_XFORM_MODE, e, a) }, init: function (e, a, b) { this.cfg = this.cfg.extend(b); this._xformMode = e; this._key = a; this.reset() }, reset: function () { t.reset.call(this); this._doReset() }, process: function (e) { this._append(e); return this._process() },
        finalize: function (e) { e && this._append(e); return this._doFinalize() }, keySize: 4, ivSize: 4, _ENC_XFORM_MODE: 1, _DEC_XFORM_MODE: 2, _createHelper: function (e) { return { encrypt: function (b, k, d) { return ("string" == typeof k ? c : a).encrypt(e, b, k, d) }, decrypt: function (b, k, d) { return ("string" == typeof k ? c : a).decrypt(e, b, k, d) } } }
    }); d.StreamCipher = v.extend({ _doFinalize: function () { return this._process(!0) }, blockSize: 1 }); var b = p.mode = {}, x = function (e, a, b) {
        var c = this._iv; c ? this._iv = u : c = this._prevBlock; for (var d = 0; d < b; d++) e[a + d] ^=
            c[d]
    }, q = (d.BlockCipherMode = l.extend({ createEncryptor: function (e, a) { return this.Encryptor.create(e, a) }, createDecryptor: function (e, a) { return this.Decryptor.create(e, a) }, init: function (e, a) { this._cipher = e; this._iv = a } })).extend(); q.Encryptor = q.extend({ processBlock: function (e, a) { var b = this._cipher, c = b.blockSize; x.call(this, e, a, c); b.encryptBlock(e, a); this._prevBlock = e.slice(a, a + c) } }); q.Decryptor = q.extend({
        processBlock: function (e, a) {
            var b = this._cipher, c = b.blockSize, d = e.slice(a, a + c); b.decryptBlock(e, a); x.call(this,
                e, a, c); this._prevBlock = d
        }
    }); b = b.CBC = q; q = (p.pad = {}).Pkcs7 = { pad: function (a, b) { for (var c = 4 * b, c = c - a.sigBytes % c, d = c << 24 | c << 16 | c << 8 | c, l = [], n = 0; n < c; n += 4) l.push(d); c = s.create(l, c); a.concat(c) }, unpad: function (a) { a.sigBytes -= a.words[a.sigBytes - 1 >>> 2] & 255 } }; d.BlockCipher = v.extend({
        cfg: v.cfg.extend({ mode: b, padding: q }), reset: function () {
            v.reset.call(this); var a = this.cfg, b = a.iv, a = a.mode; if (this._xformMode == this._ENC_XFORM_MODE) var c = a.createEncryptor; else c = a.createDecryptor, this._minBufferSize = 1; this._mode = c.call(a,
                this, b && b.words)
        }, _doProcessBlock: function (a, b) { this._mode.processBlock(a, b) }, _doFinalize: function () { var a = this.cfg.padding; if (this._xformMode == this._ENC_XFORM_MODE) { a.pad(this._data, this.blockSize); var b = this._process(!0) } else b = this._process(!0), a.unpad(b); return b }, blockSize: 4
    }); var n = d.CipherParams = l.extend({ init: function (a) { this.mixIn(a) }, toString: function (a) { return (a || this.formatter).stringify(this) } }), b = (p.format = {}).OpenSSL = {
        stringify: function (a) {
            var b = a.ciphertext; a = a.salt; return (a ? s.create([1398893684,
                1701076831]).concat(a).concat(b) : b).toString(r)
        }, parse: function (a) { a = r.parse(a); var b = a.words; if (1398893684 == b[0] && 1701076831 == b[1]) { var c = s.create(b.slice(2, 4)); b.splice(0, 4); a.sigBytes -= 16 } return n.create({ ciphertext: a, salt: c }) }
    }, a = d.SerializableCipher = l.extend({
        cfg: l.extend({ format: b }), encrypt: function (a, b, c, d) { d = this.cfg.extend(d); var l = a.createEncryptor(c, d); b = l.finalize(b); l = l.cfg; return n.create({ ciphertext: b, key: c, iv: l.iv, algorithm: a, mode: l.mode, padding: l.padding, blockSize: a.blockSize, formatter: d.format }) },
        decrypt: function (a, b, c, d) { d = this.cfg.extend(d); b = this._parse(b, d.format); return a.createDecryptor(c, d).finalize(b.ciphertext) }, _parse: function (a, b) { return "string" == typeof a ? b.parse(a, this) : a }
    }), p = (p.kdf = {}).OpenSSL = { execute: function (a, b, c, d) { d || (d = s.random(8)); a = w.create({ keySize: b + c }).compute(a, d); c = s.create(a.words.slice(b), 4 * c); a.sigBytes = 4 * b; return n.create({ key: a, iv: c, salt: d }) } }, c = d.PasswordBasedCipher = a.extend({
        cfg: a.cfg.extend({ kdf: p }), encrypt: function (b, c, d, l) {
            l = this.cfg.extend(l); d = l.kdf.execute(d,
                b.keySize, b.ivSize); l.iv = d.iv; b = a.encrypt.call(this, b, c, d.key, l); b.mixIn(d); return b
        }, decrypt: function (b, c, d, l) { l = this.cfg.extend(l); c = this._parse(c, l.format); d = l.kdf.execute(d, b.keySize, b.ivSize, c.salt); l.iv = d.iv; return a.decrypt.call(this, b, c, d.key, l) }
    })
}();
(function () {
    for (var u = CryptoJS, p = u.lib.BlockCipher, d = u.algo, l = [], s = [], t = [], r = [], w = [], v = [], b = [], x = [], q = [], n = [], a = [], c = 0; 256 > c; c++) a[c] = 128 > c ? c << 1 : c << 1 ^ 283; for (var e = 0, j = 0, c = 0; 256 > c; c++) { var k = j ^ j << 1 ^ j << 2 ^ j << 3 ^ j << 4, k = k >>> 8 ^ k & 255 ^ 99; l[e] = k; s[k] = e; var z = a[e], F = a[z], G = a[F], y = 257 * a[k] ^ 16843008 * k; t[e] = y << 24 | y >>> 8; r[e] = y << 16 | y >>> 16; w[e] = y << 8 | y >>> 24; v[e] = y; y = 16843009 * G ^ 65537 * F ^ 257 * z ^ 16843008 * e; b[k] = y << 24 | y >>> 8; x[k] = y << 16 | y >>> 16; q[k] = y << 8 | y >>> 24; n[k] = y; e ? (e = z ^ a[a[a[G ^ z]]], j ^= a[a[j]]) : e = j = 1 } var H = [0, 1, 2, 4, 8,
        16, 32, 64, 128, 27, 54], d = d.AES = p.extend({
            _doReset: function () {
                for (var a = this._key, c = a.words, d = a.sigBytes / 4, a = 4 * ((this._nRounds = d + 6) + 1), e = this._keySchedule = [], j = 0; j < a; j++) if (j < d) e[j] = c[j]; else { var k = e[j - 1]; j % d ? 6 < d && 4 == j % d && (k = l[k >>> 24] << 24 | l[k >>> 16 & 255] << 16 | l[k >>> 8 & 255] << 8 | l[k & 255]) : (k = k << 8 | k >>> 24, k = l[k >>> 24] << 24 | l[k >>> 16 & 255] << 16 | l[k >>> 8 & 255] << 8 | l[k & 255], k ^= H[j / d | 0] << 24); e[j] = e[j - d] ^ k } c = this._invKeySchedule = []; for (d = 0; d < a; d++) j = a - d, k = d % 4 ? e[j] : e[j - 4], c[d] = 4 > d || 4 >= j ? k : b[l[k >>> 24]] ^ x[l[k >>> 16 & 255]] ^ q[l[k >>>
                    8 & 255]] ^ n[l[k & 255]]
            }, encryptBlock: function (a, b) { this._doCryptBlock(a, b, this._keySchedule, t, r, w, v, l) }, decryptBlock: function (a, c) { var d = a[c + 1]; a[c + 1] = a[c + 3]; a[c + 3] = d; this._doCryptBlock(a, c, this._invKeySchedule, b, x, q, n, s); d = a[c + 1]; a[c + 1] = a[c + 3]; a[c + 3] = d }, _doCryptBlock: function (a, b, c, d, e, j, l, f) {
                for (var m = this._nRounds, g = a[b] ^ c[0], h = a[b + 1] ^ c[1], k = a[b + 2] ^ c[2], n = a[b + 3] ^ c[3], p = 4, r = 1; r < m; r++) var q = d[g >>> 24] ^ e[h >>> 16 & 255] ^ j[k >>> 8 & 255] ^ l[n & 255] ^ c[p++], s = d[h >>> 24] ^ e[k >>> 16 & 255] ^ j[n >>> 8 & 255] ^ l[g & 255] ^ c[p++], t =
                    d[k >>> 24] ^ e[n >>> 16 & 255] ^ j[g >>> 8 & 255] ^ l[h & 255] ^ c[p++], n = d[n >>> 24] ^ e[g >>> 16 & 255] ^ j[h >>> 8 & 255] ^ l[k & 255] ^ c[p++], g = q, h = s, k = t; q = (f[g >>> 24] << 24 | f[h >>> 16 & 255] << 16 | f[k >>> 8 & 255] << 8 | f[n & 255]) ^ c[p++]; s = (f[h >>> 24] << 24 | f[k >>> 16 & 255] << 16 | f[n >>> 8 & 255] << 8 | f[g & 255]) ^ c[p++]; t = (f[k >>> 24] << 24 | f[n >>> 16 & 255] << 16 | f[g >>> 8 & 255] << 8 | f[h & 255]) ^ c[p++]; n = (f[n >>> 24] << 24 | f[g >>> 16 & 255] << 16 | f[h >>> 8 & 255] << 8 | f[k & 255]) ^ c[p++]; a[b] = q; a[b + 1] = s; a[b + 2] = t; a[b + 3] = n
            }, keySize: 8
        }); u.AES = p._createHelper(d)
})();

var url = 'http://localhost:54160/';
var domainApp = 'http://localhost:54160/';
var lacviet = {
    load: function (color) {
        var decryptedColor = CryptoJS.AES.decrypt(color, "Secret Passphrase").toString(CryptoJS.enc.Utf8);
        lacviet.initCSS();
        lacviet.initFormChat(decryptedColor);
        //lacviet.tempPopupQnA();
        lacviet.modalContainer();
        //domainApp = decryptedUrl.slice(0, -1);

        //var modal = document.querySelector(".bot-modal");
        //var closeButton = document.querySelector(".bot-close-button");
        //function toggleModal() {
        //    modal.classList.toggle("bot-show-modal");
        //}
        //closeButton.addEventListener("click", toggleModal);
    },
    initFormChat: function (botId, color) {
        //var decryptedUrl = CryptoJS.AES.decrypt(encryptUrl, "Secret Passphrase").toString(CryptoJS.enc.Utf8);
        var styleDiv = 'opacity:1;visibility:visible;z-index:2147483639;position:fixed;bottom:0px;max-width: 100%;max-height:calc(100% - 0px);min-height:0px;min-width: 0px;background-color:transparent; border:0px;overflow:hidden;right: 0px;transition: none 0s ease 0s!important;';
        $("<div id='dialog-form-bot' style=\"" + styleDiv + "\"></div>").appendTo("body");
        var html = '',
        styleIframeCustom = 'style= "width: 100%;';
        styleIframeCustom += 'height: 100%;';//neu mo 100%
        styleIframeCustom += 'min-height: 0px;'
        styleIframeCustom += 'min-width: 0px;'
        styleIframeCustom += 'margin: 0px;'
        styleIframeCustom += 'padding: 0px;'
        styleIframeCustom += 'background-image: none;'
        styleIframeCustom += 'background-position: 0% 0%;'
        styleIframeCustom += 'background-size: initial;'
        styleIframeCustom += 'background-attachment: scroll;'
        styleIframeCustom += 'background-origin: initial;'
        styleIframeCustom += 'background-clip: initial;'
        styleIframeCustom += 'background-color: rgba(0, 0, 0, 0);'
        styleIframeCustom += 'border-width: 0px;'
        styleIframeCustom += 'float: none;'
        styleIframeCustom += 'position: absolute;'
        styleIframeCustom += 'top: 0px;'
        styleIframeCustom += 'left: 0px;'
        styleIframeCustom += 'bottom: 0px;'
        styleIframeCustom += 'right: 0px;'
        styleIframeCustom += 'z-index: 5;'
        styleIframeCustom += 'transition: none 0s ease 0s!important;"';
        url = url + "LcChatBox/Index";
        html += '<span style="vertical-align:bottom;width:0px;height:0px">';
        html += '<iframe name="f12691cd05677d" frameborder="0"allowtransparency="true"allowfullscreen="true"scrolling="no"';
        html += 'allow="encrypted-media"title=""src="' + url + '"';
        html += styleIframeCustom;
        //html += 'style="border: none;visibility: visible;width: 288pt;height: 378pt;border-radius: 9pt;bottom: 63pt;padding: 0px;';
        //html += 'position: fixed;right: 9pt;top: auto;z-index: 2147483646;max-height:0px;"';
        html += 'class="fb_customer_chat_bounce_out_v2" id="dialog_iframe"></iframe>';
        html += '</span>';
        html += '<div class="fb_dialog fb_dialog_advanced fb_customer_chat_bubble_pop_in fb_customer_chat_bubble_animated_with_badge fb_customer_chat_bubble_animated_no_badge" style="background: white; border-radius: 50%; bottom: 18pt; display: inline; height: 45pt; padding: 0px; position: fixed; right: 18pt; top: auto; width: 45pt; z-index: 3;">';
        html += '<div class="fb_dialog_content" style="background: none;">';
        html += '<div tabindex="0" role="button" style="cursor: pointer; outline: none;">';
        //html += '<svg width="60px" height="60px" viewBox="0 0 60 60">';
        //html += '<svg x="0" y="0" width="60px" height="60px"><g stroke="none" stroke-width="1" fill="none" fill-rule="evenodd"><g><circle fill="#5969ff" style="fill:' + color + '" cx="30" cy="30" r="30"></circle><svg x="10" y="10"><g transform="translate(0.000000, -10.000000)" fill="#FFFFFF"><g id="logo" transform="translate(0.000000, 10.000000)"><path d="M20,0 C31.2666,0 40,8.2528 40,19.4 C40,30.5472 31.2666,38.8 20,38.8 C17.9763,38.8 16.0348,38.5327 14.2106,38.0311 C13.856,37.9335 13.4789,37.9612 13.1424,38.1098 L9.1727,39.8621 C8.1343,40.3205 6.9621,39.5819 6.9273,38.4474 L6.8184,34.8894 C6.805,34.4513 6.6078,34.0414 6.2811,33.7492 C2.3896,30.2691 0,25.2307 0,19.4 C0,8.2528 8.7334,0 20,0 Z M7.99009,25.07344 C7.42629,25.96794 8.52579,26.97594 9.36809,26.33674 L15.67879,21.54734 C16.10569,21.22334 16.69559,21.22164 17.12429,21.54314 L21.79709,25.04774 C23.19919,26.09944 25.20039,25.73014 26.13499,24.24744 L32.00999,14.92654 C32.57369,14.03204 31.47419,13.02404 30.63189,13.66324 L24.32119,18.45264 C23.89429,18.77664 23.30439,18.77834 22.87569,18.45674 L18.20299,14.95224 C16.80079,13.90064 14.79959,14.26984 13.86509,15.75264 L7.99009,25.07344 Z"></path></g></g></svg></g></g></svg>';
        //html += '</svg>';

        html += '<svg width="60px" height="60px" color="rgb(67, 132, 245);" class="lc-1mpchac" viewBox="0 0 32 32"><path d="M11.9,26H8.6c-3.3,0-6.2-2.4-6.4-5.8C1.9,17,2,12,2.2,8.9c0.3-3.1,2.8-5.4,5.8-5.6c4.8-0.3,11-0.3,15.9,0 c3.1,0.2,5.6,2.6,5.8,5.7c0.2,3.1,0.2,8.2,0,11.2c-0.3,3.4-3.2,5.8-6.4,5.8h-2.5c-0.4,0-0.9,0.2-1.2,0.4l-6.1,4.4 C12.9,31.3,12,30.9,12,30v-4H11.9z"></path></svg>'

        html += '</div>';
        html += '</div>';
        html += '</div>';
        $("#dialog-form-bot").empty().append(html);
    },
    initCSS: function () {
        //var decryptedUrl = CryptoJS.AES.decrypt(encryptUrl, "Secret Passphrase").toString(CryptoJS.enc.Utf8);
        var headID = document.getElementsByTagName('head')[0];
        var link = document.createElement('link');
        link.type = 'text/css';
        link.rel = 'stylesheet';
        headID.appendChild(link);
        link.href = domainApp + "assets/livechat/css/chatbox-dialog.css";
    },
    modalContainer: function () {
        var tempModalContainer = '';
        tempModalContainer += '<div class="bot-modal" style="visibility:hidden; z-index:999999999999">';
        tempModalContainer += '        <div class="bot-modal-content large_bg">';
        tempModalContainer += '            <span class="bot-close-button">×</span>';
        tempModalContainer += '            <div id="bot-iframe">';
        tempModalContainer += '            </div>';
        tempModalContainer += '        </div>';
        tempModalContainer += '</div>';
        $(tempModalContainer).appendTo("body");
    },
    tempPopupQnA: function () {
        $('<div class="modal fade" id="cb-ques-popup" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true" style="display:none;z-index:999999999999"></div>').appendTo("body");
        var html = '';
        html += ' <div class="modal-dialog modal-question-answer modal-sm modal-lg modal-md" role="document">';
        html += '    <div class="modal-content">';
        html += '        <div class="modal-header">';
        html += '            <button type="button" class="close" data-dismiss="modal">&times;</button>';
        html += '            <h5 class="modal-title" id="exampleModalLabel">Chi tiết câu hỏi</h5>';
        html += '        </div>';
        html += '        <div class="modal-body">';
        html += '            <form>';
        html += '                <div class="form-group">';
        html += '                    <i style="margin-right:14px" class="fa fa-question-circle icon-question-detail"></i>';
        html += '                    <label for="exampleFormControlTextarea1">CÂU HỎI</label>';
        html += '                    <div class="question-detail" id="cb-question">';
        html += '                    </div>';
        html += '                </div>';
        html += '                <div class="form-group">';
        html += '                    <i style="margin-right:10px" class="fa fa-comments icon-question-detail"></i>';
        html += '                    <label for="exampleFormControlTextarea1">TRẢ LỜI</label>';
        html += '                    <div class="answer-detail" id="cb-answer">';
        html += '                    </div>';
        html += '                </div>';
        html += '                <div class="form-group" id="cb-lst-article">';
        html += '                    <i style="margin-right:12px" class="fa fa-book icon-question-detail"></i>';
        html += '                    <label for="exampleFormControlTextarea1">ĐIỀU LUẬT LIÊN QUAN THAM KHẢO</label>';
        html += '                    <div class="text-question-detail">';
        html += '                        <div id="cb-msg"></div>';
        html += '                        <div id="cb-info-article"></div>';
        html += '                    </div>';
        html += '                </div>';
        html += '            </form>';
        html += '        </div>';
        html += '    </div>';
        html += '</div>';
        $("#cb-ques-popup").empty().append(html);
    }
};
$('body').on('click', '.fb_dialog', function (e) {
    if ($("#dialog_iframe").hasClass("fb_customer_chat_bounce_out_v2")) {
        $("#dialog_iframe").removeClass('fb_customer_chat_bounce_out_v2').addClass('fb_customer_chat_bounce_in_v2');
        //$("#dialog_iframe").removeClass('fb_customer_chat_bounce_out_v2').addClass('fb_customer_chat_bounce_in_v2');
        if ($(parent.window).width() <= 768) {
            //console.log($(parent.window).width())
            $('#dialog-form-bot').css('max-height', '100%');
            $('#dialog-form-bot').css('width', '100%');
            $('#dialog-form-bot').css('height', '100%');
            $('#dialog-form-bot').css('right', '0');
            $('#dialog-form-bot').css('top', '0');
            $('#dialog-form-bot').css('bottom', '0');
            var frame = document.getElementById('dialog_iframe');
            frame.contentWindow.postMessage($(parent.window).width(), domainApp);
        }
        else {
            $('#dialog-form-bot').css('width', '382px');
            $('#dialog-form-bot').css('height', '652px');
        }

        setTimeout(function () {
            // bung chiều cao ô chatbox
            $('#dialog_iframe').css('max-height', '100%');
            // init message card getstarted
            var frame = document.getElementById('dialog_iframe');
            frame.contentWindow.postMessage('init', domainApp);
        }, 200)
    }
    else if ($("#dialog_iframe").hasClass("fb_customer_chat_bounce_in_v2")) {
        $("#dialog_iframe").removeClass('fb_customer_chat_bounce_in_v2').addClass('fb_customer_chat_bounce_out_v2');
        setTimeout(function () {
            $('#dialog_iframe').css('max-height', '0px');
        }, 200)
    }
})

var eventMethod = window.addEventListener ? "addEventListener" : "attachEvent";
var eventer = window[eventMethod];
var messageEvent = eventMethod == "attachEvent" ? "onmessage" : "message";
// Nghe tin nhắn từ iframe gởi tới trang cha khi bấm close 
eventer(messageEvent, function (e) {
    var key = e.message ? "message" : "data";
    var data = e[key];
    //console.log(e.data)
    //console.log(e.origin)
    //console.log(domainApp)
    if (e.data == 'close') {
        // có tín hiệu đóng
        $('.fb_dialog').click();
    } else {
        //console.log(e.data)
        //if (e.data != undefined) {
        //    var modal = document.querySelector(".bot-modal");
        //    $("#bot-iframe").empty().append('<iframe style="width:49rem" width="100" height="378"frameborder="0"allowtransparency="true"allowfullscreen="true" src="' + e.data + '"></iframe>');
        //    modal.classList.toggle("bot-show-modal");
        //}
    }
    //if (e.origin === domainApp.replace("/tiengviet", "")) {
    //    if (e.data != 'close') {
    //        //call function page QnA
    //        window.GetQuesDetailPopup(event.data)
    //    }
    //};
}, false);